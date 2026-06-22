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

export type QuestionType = 'dropdown-no-repeat' | 'free-text-40' | 'dropdown-repeat';

export const QUESTION_TYPES: { value: QuestionType; label: string }[] = [
  { value: 'dropdown-no-repeat', label: 'Dropdown (no image + no repeat)' },
  { value: 'free-text-40', label: 'Free text max 40 characters' },
  { value: 'dropdown-repeat', label: 'Dropdown (No image - repeat)' },
];

export interface QuestionAddon {
  addonName: string | null; // null = custom (not from supplier addon list)
  answerName: string;
  answerNameEn: string;
}

export interface GroupQuestion {
  id: string;
  text: string;
  textEn: string;
  type: QuestionType;
  mandatory: boolean;
  nextQuestion: string; // ID of next question to show, empty = none
  addons: QuestionAddon[];
}

export interface QuestionGroup {
  name: string;
  questions: GroupQuestion[];
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

  isAddon: boolean;

  // Step 2
  questionGroup: QuestionGroup | null;

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
  printingSize: string;
  additionalInformation: string;
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
  isAddon: false,
  questionGroup: null,
  currencyEndPrice: '',
  supplierCurrency: '',
  brand: '',
  deliveryInformation: '',
  delivTimeMin: '',
  delivTimeMax: '',
  sizeInfo: '',
  materialInfo: '',
  printingSize: '',
  additionalInformation: '',
  tags: [],
  categories: [],
};
