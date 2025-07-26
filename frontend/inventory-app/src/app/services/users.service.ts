import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import {
   LoginPayload,
   LoginResponse,
   ProductPayload,
   ProductResponse,
   ProductStatusResponse,
   RegisterPayload,
   RegisterResponse,
   TypeOfManufacturingResponse,
} from '../models/auth.model';

@Injectable({
   providedIn: 'root',
})
export class UsersService {
   private http = inject(HttpClient);
   private baseUrl = environment.apiUrl;

   login(credentials: LoginPayload): Observable<LoginResponse> {
      return this.http.post<LoginResponse>(
         `${this.baseUrl}/users/login`,
         credentials
      );
   }

   register(credentials: RegisterPayload): Observable<RegisterResponse> {
      return this.http.post<RegisterResponse>(
         `${this.baseUrl}/users/register`,
         credentials
      );
   }
}
