export interface FullRequest {
  citizenEmail: string;
  citizenName: string;
  citizenPhoneNumber: string;
  details: string;
  fields: { name: string; values: string }[];
  requestId: number;
  requestState: string;
}
