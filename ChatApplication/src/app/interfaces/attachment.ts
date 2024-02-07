import { Guid } from "guid-typescript";

export interface Attachment {
    attachmentId : Guid | null;
    fileName : string;
}
