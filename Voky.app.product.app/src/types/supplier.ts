export interface SupplierAddon {
  id: string;
  name: string;
  productNr?: string;
  supplierArtNr?: string;
}

export interface Supplier {
  supplierNr: string;
  supplierName: string;
  supplierCurrency: string;
  addons: SupplierAddon[];
}

export const suppliers: Supplier[] = [
  {
    supplierNr: 'SUP-001',
    supplierName: 'Nordic Print AB',
    supplierCurrency: 'SEK',
    addons: [
      { id: 'SUP-001-A1', name: 'Screen printing (1 color)' },
      { id: 'SUP-001-A2', name: 'Screen printing (full color)' },
      { id: 'SUP-001-A3', name: 'Embroidery' },
      { id: 'SUP-001-A4', name: 'Heat transfer' },
      { id: 'SUP-001-A5', name: 'Individual packaging' },
      { id: 'SUP-001-A6', name: 'Gift box' },
    ],
  },
  {
    supplierNr: 'SUP-002',
    supplierName: 'PromoGear Europe',
    supplierCurrency: 'EUR',
    addons: [
      { id: 'SUP-002-A1', name: 'Laser engraving' },
      { id: 'SUP-002-A2', name: 'Pad printing' },
      { id: 'SUP-002-A3', name: 'Digital print' },
      { id: 'SUP-002-A4', name: 'Branded hang tag' },
      { id: 'SUP-002-A5', name: 'Custom color matching' },
    ],
  },
  {
    supplierNr: 'SUP-003',
    supplierName: 'Shenzhen Merch Co.',
    supplierCurrency: 'USD',
    addons: [
      { id: 'SUP-003-A1', name: 'Silkscreen print' },
      { id: 'SUP-003-A2', name: 'Debossing' },
      { id: 'SUP-003-A3', name: 'Embossing' },
      { id: 'SUP-003-A4', name: 'Custom zipper pull' },
      { id: 'SUP-003-A5', name: 'RFID lining' },
      { id: 'SUP-003-A6', name: 'Polybag packaging' },
      { id: 'SUP-003-A7', name: 'Retail hang tag' },
    ],
  },
  {
    supplierNr: 'SUP-004',
    supplierName: 'Baltic Brands OÜ',
    supplierCurrency: 'EUR',
    addons: [
      { id: 'SUP-004-A1', name: 'Woven label' },
      { id: 'SUP-004-A2', name: 'Printed label' },
      { id: 'SUP-004-A3', name: 'Neck label removal' },
      { id: 'SUP-004-A4', name: 'Custom color' },
    ],
  },
  {
    supplierNr: 'SUP-005',
    supplierName: 'Stockholm Style HB',
    supplierCurrency: 'SEK',
    addons: [
      { id: 'SUP-005-A1', name: 'Foil stamping' },
      { id: 'SUP-005-A2', name: 'UV spot coating' },
      { id: 'SUP-005-A3', name: 'Custom ribbon' },
      { id: 'SUP-005-A4', name: 'Magnetic closure box' },
      { id: 'SUP-005-A5', name: 'Tissue paper wrapping' },
    ],
  },
  {
    supplierNr: '1050425',
    supplierName: 'Mid Ocean Brands B.V',
    supplierCurrency: 'SEK',
    addons: [
      { id: '1050425-A1', name: 'Rundscreen 180mm x 45mm 1-färg', productNr: 'W4200280', supplierArtNr: 'ROUNDSCREEN 180mm x 45mm 1-färg, inkl hantering' },
      { id: '1050425-A2', name: 'Tampotryck 35mm x 40mm 1-färg', productNr: 'W4200281', supplierArtNr: 'TAMPOPRINT 35mm x 40mm 1-färg, inkl hantering' },
      { id: '1050425-A3', name: 'Tampotryck 35mm x 40mm 2-färg', productNr: 'W4200282', supplierArtNr: 'TAMPOPRINT 35mm x 40mm 2-färg, inkl hantering' },
      { id: '1050425-A4', name: 'Tampotryck 35mm x 40mm 3-färg', productNr: 'W4200283', supplierArtNr: 'TAMPOPRINT 35mm x 40mm 3-färg, inkl hantering' },
      { id: '1050425-A5', name: 'Screentryck 90 x 50 mm 1 Färg', productNr: 'W4200284', supplierArtNr: 'SCREENPRINT 90x50mm 1-färg, inkl hantering' },
      { id: '1050425-A6', name: 'Screentryck 90 x 50 mm 2 Färger', productNr: 'W4200285', supplierArtNr: 'SCREENPRINT 90x50mm 2-färger, inkl hantering' }
    ]
  },
];
