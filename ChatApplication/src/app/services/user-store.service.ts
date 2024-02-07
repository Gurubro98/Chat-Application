import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserStoreService {
  private Name$ = new BehaviorSubject<string>('');
  private role$ = new BehaviorSubject<string>('');
  private Id$ = new BehaviorSubject<string>('');
  private timezone$ = new BehaviorSubject<any>({});
  constructor() {}

  public getOffset() {
    return this.timezone$.asObservable();
  }

  public setOffset(timezone: any) {
    this.timezone$.next(timezone);
  }

  public getRoleFromToken() {
    return this.role$.asObservable();
  }

  public setRoleForStore(role: string) {
    this.role$.next(role);
  }

  public getIdFromToken() {
    return this.Id$.asObservable();
  }

  public setIdForStore(Id: string) {
    this.Id$.next(Id);
  }

  public getNameFromToken() {
    return this.Name$.asObservable();
  }

  public setNameForStore(name: string) {
    this.Name$.next(name);
  }
}
