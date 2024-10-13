import React from 'react';
import {NavMenu} from "./NavMenu";
import {Container} from "reactstrap";
import {t} from "i18next";


export const Home = () => {
    document.title = t("portal:title")

    return (
        <div>
            <NavMenu/>
            <Container>
                <span>{t("portal:title")}</span>
            </Container>
        </div>

    );
}
