import React from 'react';
import {useRecoilValue} from "recoil";
import {AuthorizeUserDto} from "../../models/dto/accounts/AuthorizeUserDto";
import {authAtom} from "../../state/auth";

export const AuthorizedUserInfo = ({setProfileModal, withStadiumGroupName = true}: any) => {
    const auth = useRecoilValue<AuthorizeUserDto | null>(authAtom);


    return (auth !== null ? <div className="authorized-user-info" onClick={() => setProfileModal(true)}>
        <span className="authorized-user-info-name">{auth.fullName}</span>
        {withStadiumGroupName && <span className="authorized-user-info-stadium-group">{auth.stadiumGroupName}</span>}
    </div> : null)
}