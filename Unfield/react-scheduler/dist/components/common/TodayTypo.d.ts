interface TodayTypoProps {
    date: Date;
    onClick?(day: Date): void;
    locale: Locale;
}
declare const TodayTypo: ({ date, onClick, locale }: TodayTypoProps) => JSX.Element;
export default TodayTypo;
