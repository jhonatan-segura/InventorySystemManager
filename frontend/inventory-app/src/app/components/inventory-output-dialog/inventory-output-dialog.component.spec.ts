import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryOutputDialogComponent } from './inventory-output-dialog.component';

describe('InventoryOutputDialogComponent', () => {
  let component: InventoryOutputDialogComponent;
  let fixture: ComponentFixture<InventoryOutputDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InventoryOutputDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InventoryOutputDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
