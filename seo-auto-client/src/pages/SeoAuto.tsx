import React, { useState } from "react";
import { Typography, Row, Col, message } from "antd";
import SeoAutoForm from "../components/SeoAutoForm";
import SeoAutoResults from "../components/SeoAutoResults";
import { getSeoAuto } from "../apis/seoAutoApi";
import { ISeoAutoSearchEngineResponse } from "../apis/dtos/ISeoAutoSearchEngineResponse";

const { Title } = Typography;

function SeoAuto() {
    const [loading, setLoading] = useState(false);
    const [results, setResults] = useState<ISeoAutoSearchEngineResponse[]>([]);

    const handleSubmit = async (values: {
        keyword: string;
        targetUrl: string;
        searchEngines: number[];
    }) => {
        setLoading(true);
        setResults([]);
        try {
            const result = await getSeoAuto(values);
            setResults(result);
            message.success("SEO Auto fetched successfully");
        } catch (error) {
            message.error("Failed to fetch SEO Auto. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <Row justify="center" style={{ paddingTop: "40px" }}>
            <Col xs={20} sm={20} md={16} lg={12} xl={12}>
                <div className="SeoAuto">
                    <Title
                        level={1}
                        style={{ textAlign: "center", marginBottom: "24px" }}
                    >
                        SEO Auto
                    </Title>
                    <SeoAutoForm onSubmit={handleSubmit} loading={loading} />
                </div>
                {results.length > 0 && <SeoAutoResults results={results} />}
            </Col>
        </Row>
    );
}

export default SeoAuto;
