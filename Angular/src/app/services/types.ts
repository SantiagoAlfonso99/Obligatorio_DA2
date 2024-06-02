export interface CategoryReturnModel{
    int:number;
    name:string;
}

export interface SessionReturnModel{
    token:string;
}

export interface InvitationReturnModel{
    id:number;
    name : string;
    email : string;
    deadLine : Date;
    status : string;
}

export interface BuildingReturnModel{
    managerName : string;
    id : number;
    name : string;
    address : string;
    latitude : number;
    longitude : number;
    constructionCompany : string;
    commonExpenses : number;
}

export interface ManagerReturnModel{
    id : number;
    email : number;
    name : string;
}

export interface AdminReturnModel{
    id : number;
    name : string;
    lastName : string;
    email : string;
}