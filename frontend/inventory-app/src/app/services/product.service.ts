import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import {
   InventoryOutputPayload,
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
export class ProductService {
   private http = inject(HttpClient);
   private baseUrl = environment.apiUrl;

   getProducts(): Observable<ProductResponse[]> {
      return this.http.get<ProductResponse[]>(`${this.baseUrl}/products`);
   }

   setProductAsFaulty(id: string): Observable<any[]> {
      return this.http.patch<any[]>(
         `${this.baseUrl}/products/set-as-faulty/${id}`,
         {}
      );
   }

   moveToOutput(body: InventoryOutputPayload): Observable<any[]> {
      return this.http.post<any[]>(
         `${this.baseUrl}/products/move-to-output`,
         body
      );
   }

   registerProduct(body: ProductPayload): Observable<any[]> {
      return this.http.post<any[]>(`${this.baseUrl}/products`, body);
   }

   updateProduct(id: string, body: ProductPayload): Observable<any[]> {
      return this.http.put<any[]>(`${this.baseUrl}/products/${id}`, body);
   }

   deleteProduct(id: string): Observable<any[]> {
      return this.http.delete<any[]>(`${this.baseUrl}/products/${id}`);
   }
}
