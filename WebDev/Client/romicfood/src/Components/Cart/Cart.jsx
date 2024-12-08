import React from 'react';
import './cart.css';
import { FiTrash2 } from 'react-icons/fi'; // Иконка для удаления товара

function Cart({ items }) {
    return (
        <div className="cart">
            <h2 className="cart_title">Корзина</h2>
            <div className="cart_items">
                {items.length > 0 ? (
                    items.map((item, index) => (
                        <div key={index} className="cart_item">
                            <img src={item.image} alt={item.name} className="cart_item_image" />
                            <div className="cart_item_details">
                                <h4 className="cart_item_name">{item.name}</h4>
                                <p className="cart_item_price">{item.price} ₽</p>
                            </div>
                            <button className="cart_item_remove">
                                <FiTrash2 />
                            </button>
                        </div>
                    ))
                ) : (
                    <p className="cart_empty">Корзина пуста</p>
                )}
            </div>
            {items.length > 0 && (
                <div className="cart_summary">
                    <h3>Итого:</h3>
                    <p className="cart_total_price">{items.reduce((total, item) => total + item.price, 0)} ₽</p>
                    <button className="cart_checkout_button">Оформить заказ</button>
                </div>
            )}
        </div>
    );
}

export default Cart;