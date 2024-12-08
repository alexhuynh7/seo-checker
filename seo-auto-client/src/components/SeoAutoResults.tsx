import React from "react";
import { Typography, List } from "antd";
import { ISeoAutoSearchEngineResponse } from "../apis/dtos/ISeoAutoSearchEngineResponse";

const { Title, Text } = Typography;

interface SeoAutoResultsProps {
    results: ISeoAutoSearchEngineResponse[];
}

const SeoAutoResults: React.FC<SeoAutoResultsProps> = ({ results }) => {
    return (
        <div style={{ marginTop: "24px" }}>
            {results.map((se, seIndex) => (
                <div
                    key={seIndex}
                    style={{
                        marginBottom: "24px",
                        padding: "16px",
                        boxShadow: "0 4px 8px rgba(0,0,0,0.1)",
                        borderRadius: "8px",
                    }}
                >
                    <Title level={2} style={{ marginBottom: "16px", textAlign: "left" }}>
                        Auto on {se.searchEngine}
                    </Title>
                    <List
                        dataSource={se.result}
                        renderItem={(r) => (
                            <List.Item style={{ textAlign: "left" }}>
                                <Text>
                                    {r.index}. {r.url}
                                </Text>
                            </List.Item>
                        )}
                    />
                </div>
            ))}
        </div>
    );
};

export default SeoAutoResults;