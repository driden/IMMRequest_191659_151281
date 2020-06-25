export interface Request {
  details: string;
  email: string;
  name: string;
  phone: string;
  typeId: number;
  additionalFields: { name: string; values: any }[];
}
