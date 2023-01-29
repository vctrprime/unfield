import React from 'react';
import {useRecoilValue} from "recoil";
import {AuthorizeUserDto} from "../../models/dto/accounts/AuthorizeUserDto";
import {authAtom} from "../../state/auth";

export const AuthorizedUserInfo = ({ setProfileModal, withLegalName = true }: any) => {
    const auth = useRecoilValue<AuthorizeUserDto | null>(authAtom);
    
    
    return (auth !== null ? <div className="authorized-user-info" onClick={() => setProfileModal(true)}>
        <span className="authorized-user-info-name">{auth.fullName}</span>
        {withLegalName && <span className="authorized-user-info-legal">{auth.legalName}</span>}
    </div> : null)
}