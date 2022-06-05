export class ListaDados<T>
{
    public list : T[];
    public totalResults: number;
    public pageIndex: number;
    public pageSize: number;
    public query: string;
}