export const parse =(point: number) => {
    const parts = point.toString().replace(",", ".").split(".");
    const hour = parseInt(parts[0])//$"{Int32.Parse( parts[ 0 ] ):D2}";

    return parts.length == 1 ? `${parts[0].length === 1 ? "0" + hour : hour}:00` : `${parts[0].length === 1 ? "0" + hour : hour}:30`;
}