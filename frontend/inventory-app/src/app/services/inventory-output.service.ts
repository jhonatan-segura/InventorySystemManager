import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import {
   InventoryOutputPayload,
   InventoryOutputResponse,
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
export class InventoryOutputService {
   private http = inject(HttpClient);
   private baseUrl = environment.apiUrl;

   getInventoryOutputs(): Observable<InventoryOutputResponse[]> {
      return this.http.get<InventoryOutputResponse[]>(
         `${this.baseUrl}/inventory-outputs`
      );
   }

   registerProduct(body: ProductPayload): Observable<any[]> {
      return this.http.post<any[]>(`${this.baseUrl}/inventory-outputs`, body);
   }

   updateProduct(id: string, body: InventoryOutputPayload): Observable<any[]> {
      return this.http.put<any[]>(
         `${this.baseUrl}/inventory-outputs/${id}`,
         body
      );
   }

   deleteProduct(id: string): Observable<any[]> {
      return this.http.delete<any[]>(`${this.baseUrl}/inventory-outputs/${id}`);
   }
}
