import { Guid } from "guid-typescript";
import { Register } from "./register";

export interface Mutual {
    mutualId : string | null;
    userId : string | null;
    user : Register | null;
    mutual : Register | null;
    isOnline : boolean;
    isActive : boolean;
    count : number;
}

