export interface CityDto {
    id: number;
    name: string;
    shortName: string | null;
    regionName: string | null;
    regionShortName: string | null;
    countryName: string | null;
    countryShortName: string | null;
    displayName: string;
}