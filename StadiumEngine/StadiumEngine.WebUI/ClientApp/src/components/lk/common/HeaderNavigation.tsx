import React, {useEffect, useState} from 'react';
import {useRecoilState} from "recoil";
import {lockerRoomsAtom} from "../../../state/offers/lockerRooms";
import {LockerRoomDto} from "../../../models/dto/offers/LockerRoomDto";
import {t} from "i18next";
import {useNavigate} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";
import {Dropdown, Icon} from "semantic-ui-react";
import {FieldDto} from "../../../models/dto/offers/FieldDto";
import {fieldsAtom} from "../../../state/offers/fields";
import {InventoryDto} from "../../../models/dto/offers/InventoryDto";
import {inventoriesAtom} from "../../../state/offers/inventories";
import {PriceGroupDto} from "../../../models/dto/rates/PriceGroupDto";
import {priceGroupsAtom} from "../../../state/rates/priceGroups";
import {IRatesService} from "../../../services/RatesService";
import {TariffDto} from "../../../models/dto/rates/TariffDto";
import {tariffsAtom} from "../../../state/rates/tariffs";

export interface HeaderNavigationProps {
    routesWithBackButton: string[]
}

export const HeaderNavigation = (props: HeaderNavigationProps) => {
    const [lockerRooms, setLockerRooms] = useRecoilState<LockerRoomDto[]>(lockerRoomsAtom);
    const [fields, setFields] = useRecoilState<FieldDto[]>(fieldsAtom);
    const [inventories, setInventories] = useRecoilState<InventoryDto[]>(inventoriesAtom);

    const [priceGroups, setPriceGroups] = useRecoilState<PriceGroupDto[]>(priceGroupsAtom);
    const [tariffs, setTariffs] = useRecoilState<TariffDto[]>(tariffsAtom);

    const [data, setData] = useState<any[]>([])

    const navigate = useNavigate();

    const parts = window.location.pathname.split("/");
    const route = parts[parts.length - 2];
    const id = parts[parts.length - 1];

    const [offersService] = useInject<IOffersService>('OffersService');
    const [ratesService] = useInject<IRatesService>('RatesService');

    const BackButton = () => {
        return props.routesWithBackButton.find(r => r == route) !== undefined ?
            <Icon className="back-button"
                  name='caret left'
                  title={t(`common:back_buttons:${route.replace('-', '_')}`)}
                  onClick={() => navigate(`/lk/${parts[parts.length - 3]}/${route}`)}/> :
            <span/>
    }

    const fetch = (atom: any[], setAtom: any, getAction: any) => {
        if (atom.length === 0) {
            getAction().then((result: any[]) => {
                setAtom(result);
                setData(result.map(mapToDropDownRow));
            })
        } else {
            setData(atom.map(mapToDropDownRow));
        }
    }

    const mapToDropDownRow = (obj: any) => {
        return {key: obj.id, value: obj.id, text: obj.name}
    }

    useEffect(() => {
        if (id !== "new") {
            switch (route) {
                case "locker-rooms":
                    fetch(lockerRooms, setLockerRooms, () => offersService.getLockerRooms());
                    break;
                case "fields":
                    fetch(fields, setFields, () => offersService.getFields());
                    break;
                case "inventories":
                    fetch(inventories, setInventories, () => offersService.getInventories());
                    break;
                case "price-groups":
                    fetch(priceGroups, setPriceGroups, () => ratesService.getPriceGroups());
                    break;
                case "tariffs":
                    fetch(tariffs, setTariffs, () => ratesService.getTariffs());
                    break;
            }
        }
    }, []);

    const changeObject = (e: any, {value}: any) => {
        navigate(`/lk/${parts[parts.length - 3]}/${route}/${value}`);
    }

    return <div style={{display: "flex"}}>
        <BackButton/>
        {data.length > 0 && <Dropdown
            onChange={changeObject}
            inline
            options={data}
            defaultValue={data.find(x => x.key === parseInt(id)).value}
        />}
    </div>
}