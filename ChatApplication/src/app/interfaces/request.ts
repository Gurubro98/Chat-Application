import { Guid } from 'guid-typescript';
import { Register } from './register';
import { RequestAction } from '../helpers/RequestAction.enum';

export interface Request {
  requestId: Guid | null;
  senderId: string;
  sender: Register | null;
  receiverId: string;
  receiver: Register | null;
  status: RequestAction;
  isTakeAction: boolean;
}
