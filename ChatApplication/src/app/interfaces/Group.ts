import { Guid } from "guid-typescript";

export interface Group {
    groupId : Guid | null;
    groupName : string | null;
    userId : string | null;
}
