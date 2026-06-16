export interface MoqPricing {
  moq: number;
  endCustPrice: number;
  supplierNetPrice: number;
}

export interface ProductFormData {
  // Step 1
  productNumber: string;
  name: string;
  supplierArtNr: string;
  artNrVarianthead: string;
  headSupplier: string;
  artNrStartCost: string;
  amountStartCost: string;
  lifecycle: string;
  productDescription: string;
  moqCustomer: string;
  sendToOpti: boolean;

  // Step 2
  questions: string[];

  // Step 3
  currencyEndPrice: string;
  supplierCurrency: string;
  moqPricing: MoqPricing[];

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

export const initialFormData: ProductFormData = {
  productNumber: '',
  name: '',
  supplierArtNr: '',
  artNrVarianthead: '',
  headSupplier: '',
  artNrStartCost: '',
  amountStartCost: '0',
  lifecycle: '',
  productDescription: '',
  moqCustomer: '0',
  sendToOpti: true,
  questions: [],
  currencyEndPrice: '',
  supplierCurrency: '',
  moqPricing: [],
  brand: '',
  deliveryInformation: '',
  delivTimeMin: '',
  delivTimeMax: '',
  sizeInfo: '',
  materialInfo: '',
  tags: [],
  categories: [],
};
