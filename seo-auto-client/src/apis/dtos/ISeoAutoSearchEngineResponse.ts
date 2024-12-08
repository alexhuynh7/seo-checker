export interface ISeoAutoSearchEngineResponse {
    searchEngine: string;
    result: ISeoAutoResult[];
}

export interface ISeoAutoResult {
    url: string;
    index: number;
}