import { atom } from 'recoil';
import {PermissionsRoleDropDownData} from "../components/lk/accounts/Permissions";

const rolesAtom = atom({
    key: 'roles',
    default: [] as PermissionsRoleDropDownData[]
});

export { rolesAtom };