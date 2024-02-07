import { Guid } from "guid-typescript";
import { Register } from "./register";

export interface UserGroup {
    groupId : Guid | null;
    userId:string | null;
    user : Register;
}
