import { atom } from 'recoil';
import {UserPermissionDto} from "../models/dto/accounts/UserPermissionDto";

const permissionsAtom = atom({
    key: 'permissions',
    default: [] as UserPermissionDto[]
});

export { permissionsAtom };