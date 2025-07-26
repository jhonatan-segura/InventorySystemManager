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
export class TypeOfManufacturingService {
   private http = inject(HttpClient);
   private baseUrl = environment.apiUrl;

   getTypesOfManufacturing(): Observable<TypeOfManufacturingResponse[]> {
      return this.http.get<TypeOfManufacturingResponse[]>(
         `${this.baseUrl}/type-of-manufacturing`
      );
   }
}
