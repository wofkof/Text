import api from "../util/api";

export const getAllProducts = () => api.get('/product')
export const getProductById = (id) => api.get(`/product/${id}`)
export const createProduct = (data) => api.post('/product', data)
export const updateProduct = (id, data) => api.put(`/product/${id}`, data)
export const deleteProduct = (id) => api.delete(`/product/${id}`)