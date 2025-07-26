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
export class ProductStatusService {
   private http = inject(HttpClient);
   private baseUrl = environment.apiUrl;

   getProductStatuses(): Observable<ProductStatusResponse[]> {
      return this.http.get<ProductStatusResponse[]>(`${this.baseUrl}/product-status`);
   }
}
