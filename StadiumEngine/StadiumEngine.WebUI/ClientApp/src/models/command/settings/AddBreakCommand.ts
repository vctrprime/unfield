export interface AddBreakCommand {
    name: string,
    description: string | null,
    isActive: boolean,
    dateStart: Date,
    dateEnd: Date | null,
    startHour: string,
    endHour: string,
    selectedFields: number[]
}