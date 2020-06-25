import { AdditionalField } from './AdditionalField'

export interface Type {
  id: number;
  topicId: number;
  name: string;
  additionalFields: AdditionalField[];
  isActive: boolean;
}
