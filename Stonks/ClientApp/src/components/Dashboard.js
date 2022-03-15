import React, { Component } from 'react';
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, Legend } from 'recharts';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { stockPrices: [], loading: true };
    }

    componentDidMount() {
        this.getStockPriceData();
    }

    static renderStocksTable(stockPrices) {
        if (stockPrices == null || stockPrices.length === 0) {
            return (<p><em>No results found.</em></p>);
        }

        return (
            <div>
                <LineChart
                    width={1250}
                    height={350}
                    data={stockPrices}
                    margin={{ top: 30, right: 0, left: 0, bottom: 20 }}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="dateTime" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Line name="Open Price" type="monotone" dataKey="openPrice" stroke="#8884d8" />
                    <Line name="Close Price" type="monotone" dataKey="closePrice" stroke="#82ca9d" />
                </LineChart>

                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Trading Volume</th>
                            <th>Weighted Average Price</th>
                            <th>Open Price</th>
                            <th>Close Price</th>
                            <th>Highest Price</th>
                            <th>Lowest Price</th>
                            <th>Transaction Count</th>
                        </tr>
                    </thead>
                    <tbody>
                        {stockPrices.map(sp =>
                            <tr key={sp.id}>
                                <td>{sp.dateTime}</td>
                                <td>{sp.tradingVolume}</td>
                                <td>{sp.volumeWeightedAveragePrice}</td>
                                <td>{sp.openPrice}</td>
                                <td>{sp.closePrice}</td>
                                <td>{sp.highestPrice}</td>
                                <td>{sp.lowestPrice}</td>
                                <td>{sp.numberOfTransactions}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderStocksTable(this.state.stockPrices);

        return (
            <div>
                <h1 id="tabelLabel" >Stock Prices for Apple</h1>
                {contents}
            </div>
        );
    }

    async getStockPriceData() {
        const ticker = 'AAPL';
        const response = await fetch(`stockprice?ticker=${ticker}`);

        if (response.status === 200) {
            const data = await response.json();
            this.setState({ stockPrices: data, loading: false });
        } else {
            console.warn(`Failed to fetch stock ${ticker}. Error: ${response.status}`);
        }
    }
}
