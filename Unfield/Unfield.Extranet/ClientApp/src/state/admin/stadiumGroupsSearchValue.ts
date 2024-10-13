import {atom} from 'recoil';

const stadiumGroupsSearchValue = atom({
    key: 'stadiumGroupsSearchValue',
    default: localStorage.getItem('stadiumGroupsSearchValue') || '' as string
});

export {stadiumGroupsSearchValue};