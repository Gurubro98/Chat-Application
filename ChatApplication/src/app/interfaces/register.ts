export interface Register {
  id : string;
  name: string;
  email: string;
  password: string;
  confirmPassword: string;
  isOnline: boolean;
  isActive : boolean;
  isSendRequest : boolean;
  imageUrl : string;
}
