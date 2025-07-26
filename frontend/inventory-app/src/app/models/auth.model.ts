export interface LoginPayload {
   email: string;
   password: string;
}

export interface LoginResponse {
   token: string;
   userId: number;
   email: string;
}

export interface RegisterPayload {
   firstName: string;
   lastName: string;
   email: string;
   password: string;
}

export interface RegisterResponse {
   userId: number;
   email: string;
}

export interface ProductPayload {
   name: string;
   stockQuantity: number;
   typeOfManufacturingId: number;
   productStatusId: number;
}

export interface ProductResponse {
   id: string;
   name: string;
   stockQuantity: number;
   typeOfManufacturingId: number;
   typeOfManufacturingName: string;
   productStatusId: number;
   productStatusName: string;
}

export interface BasicEntity {
   id: string;
   name: string;
}

export interface ProductStatusResponse extends BasicEntity {}

export interface TypeOfManufacturingResponse extends BasicEntity {}

export interface BasicProduct extends BasicEntity {}

export interface InventoryOutputPayload {
   productId: string;
   stockQuantity: number;
   reason: string;
}

export interface InventoryOutputResponse {
   id: string;
   product: BasicProduct;
   stockQuantity: number;
   reason: string;
}