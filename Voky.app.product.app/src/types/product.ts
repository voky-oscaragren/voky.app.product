export interface ProductVariant {
  variantHead: string;
  productNumber: string;
  name: string;
  supplierArtNr: string;
  antalStaflingar: string;
  moqCustomer: string;
  moqPricing: MoqPricing[];
}

export function getVariantProductNumber(baseProductNumber: string, index: number): string {
  const parts = baseProductNumber.split('-')
  if (parts.length === 2) {
    const num = parseInt(parts[1], 10)
    if (!isNaN(num)) return `W-${num + index + 1}`
  }
  return `${baseProductNumber}-${index + 1}`
}

export interface MoqPricing {
  moq: number;
  freight: number;
  freightType: string;
  endCustPrice: number;
  supplierNetPrice: number;
}

export interface ProductFormData {
  // Step 1
  productNumber: string;
  name: string;
  supplierArtNr: string;
  headSupplier: string;
  artNrStartCost: string;
  amountStartCost: string;
  productDescription: string;
  moqCustomer: string;
  antalStaflingar: string;
  sendToOpti: boolean;

  // Step 2
  questions: string[];
  questionGroup: string;

  // Step 3
  currencyEndPrice: string;
  supplierCurrency: string;

  // Step 4
  brand: string;
  deliveryInformation: string;
  delivTimeMin: string;
  delivTimeMax: string;
  sizeInfo: string;
  materialInfo: string;
  tags: string[];
  categories: string[];
}

export interface QueuedProduct {
  formData: ProductFormData;
  variants: ProductVariant[];
}

export const initialFormData: ProductFormData = {
  productNumber: '',
  name: '',
  supplierArtNr: '',
  headSupplier: '',
  artNrStartCost: '',
  amountStartCost: '0',
  productDescription: '',
  moqCustomer: '0',
  antalStaflingar: '0',
  sendToOpti: true,
  questions: [],
  questionGroup: '',
  currencyEndPrice: '',
  supplierCurrency: '',
  brand: '',
  deliveryInformation: '',
  delivTimeMin: '',
  delivTimeMax: '',
  sizeInfo: '',
  materialInfo: '',
  tags: [],
  categories: [],
};
