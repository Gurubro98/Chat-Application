import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RequestService {
  constructor(private http: HttpClient) {}
  private baseUrl: string = 'https://localhost:7033/api/request/';

  getAllRequests(userId: string) {
    return this.http.get(`${this.baseUrl}GetAllRequest/` + userId);
  }

  getNumberOfRequests(userId: string) {
    return this.http.get(`${this.baseUrl}GetNumberOfRequest/` + userId);
  }
}
