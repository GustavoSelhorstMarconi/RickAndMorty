export interface ResponseApi<T> {
  isSuccess: boolean;
  data: T;
  statusCode: number;
}
