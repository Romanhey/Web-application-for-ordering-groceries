import React from 'react';
import './Cart.css';
import { FiTrash2 } from 'react-icons/fi'; // Иконка для удаления товара

function Cart({list,setList}) {


    let removeProductFromCart = (index) => {
        let newList = [...list];
        newList.splice(index, 1);
        setList(newList);
    }



    return (
        <div className="cart">
            <h2 className="cart_title">Корзина</h2>
            <div className="cart_items">
                {list.length > 0 ? (
                    list.map((item, index) => (
                        <div key={index} className="cart_item">
                            <img src={item.image} alt={item.name} className="cart_item_image" />
                            <div className="cart_item_details">
                                <h4 className="cart_item_name">{item.productName}</h4>
                                <p className="cart_item_price">{item.price} ₽</p>
                            </div>
                            <button
                                className="cart_item_remove"
                                onClick={() => removeProductFromCart(index)}
                                style={{ cursor: "pointer" }}
                            >
                                <FiTrash2
                                    style={{ cursor: "pointer" }}
                                />
                            </button>
                        </div>
                    ))
                ) : (
                    <p className="cart_empty">Корзина пуста</p>
                )}
            </div>
            {list.length > 0 && (
                <div className="cart_summary">
                    <h3>Итого:</h3>
                    <p className="cart_total_price">{list.reduce((total, item) => total + item.price, 0)} ₽</p>
                    <button className="cart_checkout_button">Оформить заказ</button>
                </div>
            )}
        </div>
    );
}

export default Cart;