import React from 'react';
import Card from "./Card";
import './CardList.css';
function CardList({}) {

    var products = [
        {
            "productId": 1,
            "productName": "string",
            "price": 10,
            "productDescription": "string",
            "categoryId": 1,
            "image":"https://kykagroup.com/wp-content/uploads/2023/07/IMG-Worlds-of-Adventure.jpg",
            "category": {
                "categoryId": 1,
                "categoryName": "string",
                "categoryDescription": "string"
            }
        },
        {
            "productId": 1,
            "productName": "string",
            "price": 10,
            "productDescription": "string",
            "categoryId": 1,
            "image":"https://kykagroup.com/wp-content/uploads/2023/07/IMG-Worlds-of-Adventure.jpg",
            "category": {
                "categoryId": 1,
                "categoryName": "string",
                "categoryDescription": "string"
            }
        },
        {
            "productId": 1,
            "productName": "string",
            "price": 10,
            "productDescription": "string",
            "categoryId": 1,
            "image":"https://kykagroup.com/wp-content/uploads/2023/07/IMG-Worlds-of-Adventure.jpg",
            "category": {
                "categoryId": 1,
                "categoryName": "string",
                "categoryDescription": "string"
            }
        },
        {
            "productId": 1,
            "productName": "string",
            "price": 10,
            "productDescription": "string",
            "categoryId": 1,
            "image":"https://kykagroup.com/wp-content/uploads/2023/07/IMG-Worlds-of-Adventure.jpg",
            "category": {
                "categoryId": 1,
                "categoryName": "string",
                "categoryDescription": "string"
            }
        },
        {
            "productId": 1,
            "productName": "string",
            "price": 10,
            "productDescription": "string",
            "categoryId": 1,
            "image":"https://kykagroup.com/wp-content/uploads/2023/07/IMG-Worlds-of-Adventure.jpg",
            "category": {
                "categoryId": 1,
                "categoryName": "string",
                "categoryDescription": "string"
            }
        }
    ]


    return (
        <div className="products-list">
            {products.map((product) => (
                <Card product={product} key={product.productId}/>
            ))}
        </div>
    );
}

export default CardList;