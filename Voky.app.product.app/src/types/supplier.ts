export interface Supplier {
  supplierNr: string;
  supplierName: string;
  supplierCurrency: string;
}

export const suppliers: Supplier[] = [
  { supplierNr: 'SUP-001', supplierName: 'Nordic Print AB', supplierCurrency: 'SEK' },
  { supplierNr: 'SUP-002', supplierName: 'PromoGear Europe', supplierCurrency: 'EUR' },
  { supplierNr: 'SUP-003', supplierName: 'Shenzhen Merch Co.', supplierCurrency: 'USD' },
  { supplierNr: 'SUP-004', supplierName: 'Baltic Brands OÜ', supplierCurrency: 'EUR' },
  { supplierNr: 'SUP-005', supplierName: 'Stockholm Style HB', supplierCurrency: 'SEK' },
];
