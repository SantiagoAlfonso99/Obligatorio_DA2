export interface CategoryReturnModel{
    int:number;
    name:string;
}

export interface InvitationReturnModel{
    id:number;
    name : string;
    email : string;
    deadLine : Date;
    status : string;
}

export interface AdminReturnModel{
    id : number;
    name : string;
    lastName : string;
    email : string;
}