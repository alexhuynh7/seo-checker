import axios from "axios";
import { ISeoAutoSearchEngineResponse } from "./dtos/ISeoAutoSearchEngineResponse";
import { ISeoAutoRequest } from "./dtos/ISeoAutoRequest";

const API_BASE_URL = "http://localhost:7236/api/Search";

export async function getSeoAuto(
    params: ISeoAutoRequest
): Promise<ISeoAutoSearchEngineResponse[]> {
    try {
        const response = await axios.get(`${API_BASE_URL}/search`, {
            params,
            paramsSerializer: {
                indexes: null,
            },
        });
        return response.data;
    } catch (error) {
        console.error("Error fetching SEO Auto:", error);
        throw error;
    }
}