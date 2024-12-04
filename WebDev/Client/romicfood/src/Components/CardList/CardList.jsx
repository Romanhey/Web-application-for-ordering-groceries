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

    const categories = ["All", "Pizza", "Burgers", "Sushi"];
    return (
        <div className="page-container">
            <h1 className="menu-title">Меню</h1>
            <div className="menu-quote">
                <span>“Cibus est vita.”</span>
                <span className="translation">(Еда — это жизнь.)</span>
            </div>
            <div className="categories">
                {categories.map((category, index) => (
                    <button className="category-button" key={index}>
                        {category}
                    </button>
                ))}
            </div>
            <div className="products-list">
                {products.map((product,index ) => (
                    <Card product={product} key={index} />
                ))}
            </div>
        </div>
    );
}

export default CardList;