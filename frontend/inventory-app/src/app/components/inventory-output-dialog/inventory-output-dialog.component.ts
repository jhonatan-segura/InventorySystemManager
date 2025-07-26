import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';

@Component({
   selector: 'app-inventory-output-dialog',
   standalone: true,
   imports: [
      CommonModule,
      FormsModule,
      MatButtonModule,
      MatInputModule,
      MatDialogModule,
   ],
   template: `
      <h2 mat-dialog-title>Salida de Inventario</h2>
      <div mat-dialog-content>
         <label>Cantidad a sacar:</label>
         <input type="number" [(ngModel)]="quantity" class="form-control mb-3" />
         <label>Razón de movimiento de producto:</label>
         <input [(ngModel)]="reason" class="form-control"/>
      </div>
      <div mat-dialog-actions class="d-flex justify-content-end gap-2 mt-3">
         <button mat-button color="warn" (click)="onCancel()">Cancelar</button>
         <button mat-raised-button color="primary" (click)="onSubmit()">
            Enviar
         </button>
      </div>
   `,
})
export class InventoryOutputDialogComponent {
   quantity: number = 0;
   reason: string = '';

   constructor(
      public dialogRef: MatDialogRef<InventoryOutputDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public productId: number
   ) {}

   onCancel(): void {
      this.dialogRef.close();
   }

   onSubmit(): void {
      this.dialogRef.close({
         quantity: this.quantity,
         reason: this.reason,
         productId: this.productId,
      });
   }
}
