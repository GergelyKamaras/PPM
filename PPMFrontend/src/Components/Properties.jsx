import { getAllPropertiesByOwnerEndpoint } from "../Config";
import { useAuth } from "../Contexts/AuthContext";
import { useState, useEffect } from "react";
import FetchData from "../DataCommunication/FetchData";

export default function Properties() {
    const [properties, setProperties] = useState([]);

    const {authUser,
        setAuthUser,
        isLoggedIn,
        setIsLoggedIn} = useAuth();

    useEffect(() => {
        // Via https://stackoverflow.com/questions/72301355/how-to-populate-select-options-from-an-api-call-in-react-js-on-page-load
        async function getProperties() {
            const data = await FetchData(getAllPropertiesByOwnerEndpoint + authUser["Id"]);
            const results = [];

            data.forEach(element => {

                let property = {};

                Object.entries(element).forEach(([key, value]) => {
                    property[key] = value;
                });

                results.push(
                    property
                );
            });

            setProperties([
                ...results
            ]);
        }

        if (isLoggedIn)
        {
            getProperties();
        }

    }, [isLoggedIn]);

    if (isLoggedIn){
        return (
            <>
                <h1>{authUser["Name"]}'s Properties</h1>
                <table className="table table-dark">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Purchase Date</th>
                            <th>Purchase Price</th>
                            <th>Size</th>
                            <th>Current Value</th>
                            <th>Total Cost</th>
                            <th>Total Revenue</th>
                            <th>Balance</th>
                        </tr>
                    </thead>
                    <tbody>
                        {properties.map(property => {
                            return (
                                <tr key={property.id}>
                                    <td>{property.name}</td>
                                    <td>{property.purchaseDate}</td>
                                    <td>{property.purchasePrice}</td>
                                    <td>{property.size}</td>
                                    <td>{property.currentValue}</td>
                                    <td>{property.totalCost}</td>
                                    <td>{property.totalRevenue}</td>
                                    <td>{property.balance}</td>
                                </tr>
                            )
                        })}
                    </tbody>
                </table>
            </>
        )
    }
    else
    {
        return (<>
        </>)
    }
}
