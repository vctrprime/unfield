export interface AddBreakCommand {
    name: string,
    description: string | null,
    isActive: boolean,
    dateStart: Date,
    dateEnd: Date | null,
    startHour: number,
    endHour: number,
    selectedFields: number[]
}