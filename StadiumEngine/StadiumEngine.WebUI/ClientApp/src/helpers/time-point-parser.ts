export const parseNumber =(point: number) => {
    const parts = point.toString().replace(",", ".").split(".");
    const hour = parseInt(parts[0])

    return parts.length == 1 ? `${parts[0].length === 1 ? "0" + hour : hour}:00` : `${parts[0].length === 1 ? "0" + hour : hour}:30`;
}

export const parseString =(point: string) => {
    if ( point == "00:00" )
    {
        point = "24:00";
    }

    const parts = point.split( ":" );
    if ( parts[ 1 ] == "30" )
    {
        return parseInt(parts[0]) + 0.5;
    }

    return parseInt(parts[0]);
}