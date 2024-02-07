import { Guid } from 'guid-typescript';
import { Group } from './Group';
import { Register } from './register';
import { Attachment } from './attachment';

export interface Messages {
  id: Guid | null;
  senderId: string;
  receiverId: string | null;
  type: string;
  content: string | null;
  timeStamp: Date;
  groupId: Guid | null;
  group: Group | null;
  sender: Register | null;
  attachmentId : Guid | null;
  attachment : Attachment | null;
  receiver: Register | null;
}
