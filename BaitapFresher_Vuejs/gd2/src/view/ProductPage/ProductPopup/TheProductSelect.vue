<template>
  <div class="product-popup-select">
    <div class="product-Popup-form">
      <div class="product-popup-select-top">
        <div class="product-popup-select-top-right">
            <div class="product-popup-select-top-right-label">
                <label for="">Chọn tính chất hàng hóa dịch vụ</label>
            </div>
        </div>
        <div class="product-popup-select-top-left">
          <div class="product-input1-right">
            <div class="product-input1-checkbox-icon ask-icon">
              <span class="product-tooltip"> Giúp (F2) </span>
            </div>

            <div
              class="product-input1-checkbox-icon question-icon"
              @click="handleClosePopupSelect"
            >
              <span class="product-tooltip"> Đóng (ESC) </span>
            </div>
          </div>
        </div>
      </div>
      <div class="product-popup-select-content">
          <div class="product-popup-select-center item1" @click="handleOpenProductPopup(1)">
              <div class="product-popup-select-center item1-icon"></div>
              <div class="product-popup-select-center-label">
                  <div class="label1" >Hàng hóa</div>
                  <div class="label2" >Sản phẩm bạn mua và bán lại cho khách hàng</div>
              </div>
          </div>
          <div class="product-popup-select-center item2" @click="handleOpenProductPopup(2)">
              <div class="product-popup-select-center item2-icon"></div>
              <div class="product-popup-select-center-label">
                  <div class="label1" >Dịch vụ</div>
                  <div class="label2" >Dịch vụ mà bạn cung cấp cho khách hàng</div>
              </div>
          </div>
          <div class="product-popup-select-center item3" @click="handleOpenProductPopup(3)">
              <div class="product-popup-select-center item3-icon"></div>
              <div class="product-popup-select-center-label">
                  <div class="label1" >Nguyên vật liệu</div>
                  <div class="label2" >Nguyên liệu đầu vào dùng cho hoạt động sản xuất, xây dựng, cung cấp dịch vụ</div>
              </div>
          </div>
          <div class="product-popup-select-center item4" @click="handleOpenProductPopup(4)">
              <div class="product-popup-select-center item4-icon"></div>
              <div class="product-popup-select-center-label">
                  <div class="label1" >Thành phẩm</div>
                  <div class="label2" >Là sản phẩm đưa ra trong quá trình sản xuất</div>
              </div>
          </div>
          <div class="product-popup-select-center item5" @click="handleOpenProductPopup(5)">
              <div class="product-popup-select-center item5-icon"></div>
               <div class="product-popup-select-center-label">
                  <div class="label1" >Công cụ dụng cụ</div>
                  <div class="label2" >Công cụ dụng cụ mua về nhập kho chưa đưa vào sử dụng</div>
              </div>
          </div> 
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props:{
    ProductID:{
      type: Number,
      default: null,
    },
  },
    methods:{

    /**
     * Hàm đóng popup tính chất
     * Nguyễn Văn Cương 15/09/2022
     */
        handleClosePopupSelect(){
            this.$emit("close-popup-selete");
        },

    /**
     * Hàm mở popup 
     * Nguyễn Văn Cương 15/09/2022
     */
        handleOpenProductPopup(value){
            if(this.ProductID){
              this.$emit("open-product-popup", this.ProductID, 2, value);
            }else{
              this.$emit("open-product-popup", null, 1, value);
            }
           
        }
    },
};
</script>

<style>
:root {
  --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}
.product-popup-select {
  z-index: 4;
  position: fixed;
  width: 100%;
  height: 100vh;
  right: 0;
  left: 0;
  top: 0;
  bottom: 0;
  background-color: rgba(12, 12, 12, 0.4);
}
.product-Popup-form {
  width: 800px;
  height: 100%;
  background-color: #fff;
  position: absolute;
  right: 0;
  border-radius: 4px;
}.product-popup-select-top{
    width: 100%;
    height: 60px;
    display: flex;
    border-bottom: 1px solid #bbbb;
}.product-popup-select-top-right-label{
    height: 60px;
    width: 4900px;
    font-size: 24px;
    font-weight: 700;  
    margin-top: 12px; 
    margin-left: 20px;
    font-family: Misa Fonts Bold;
}.product-input1-right {
  position: absolute;
  right: 0;
  display: flex;
  top: 10px;
  width: 80px;
  height: 50px;
}
.product-input1-checkbox-icon {
  width: 25px;
  height: 30px;
  margin-left: 8px;
}.ask-icon {
  background-image: var(--icon);
  background-position: -731px -27px;
  background-repeat: no-repeat;
}
.question-icon {
  background-image: var(--icon);
  background-position: -146px -141px;
  background-repeat: no-repeat;
}.product-popup-select-content{
    width: 100%;
    height: 80%;
}.product-popup-select-center{
    width: 100%;
    height: 120px;
    display: flex;
    align-items: center;
    border-bottom: 1px solid #bbbb;
    cursor: pointer ;
}.product-popup-select-center .item1-icon{
    background-image: var(--icon);
    background-position: -32px -632px;
    background-repeat: no-repeat;
    width: 85px;
    height: 85px;
    margin-right: 15px;
    margin-left: 20px;
    border: none;
}.product-popup-select-center-label{
   width: 600px;
   height: 100%;         
}.product-popup-select-center-label .label1{
    width: 100%;
    height: 25px;
    font-family: Misa Fonts Bold;
    margin-top: 40px;
    font-size: 13px;
}.product-popup-select-center-label .label2{
    font-size: 13px;
}
.product-popup-select-center .item2-icon{
    background-image: var(--icon);
    background-position: -264px -632px;
    background-repeat: no-repeat;
    width: 85px;
    height: 85px;
    margin-right: 15px;
    margin-left: 20px;
    border: none;
}.product-popup-select-center .item3-icon{
    background-image: var(--icon);
    background-position: -960px -632px;
    background-repeat: no-repeat;
    width: 85px;
    height: 85px;
    margin-right: 15px;
    margin-left: 20px;
    border: none;
}.product-popup-select-center .item4-icon{
    background-image: var(--icon);
    background-position: -148px -632px;
    background-repeat: no-repeat;
    width: 85px;
    height: 85px;
    margin-right: 15px;
    margin-left: 20px;
    border: none;
}.product-popup-select-center .item5-icon{
    background-image: var(--icon);
    background-position: -1079px -632px;
    background-repeat: no-repeat;
    width: 85px;
    height: 85px;
    margin-right: 15px;
    margin-left: 20px;
    border: none;
}.product-tooltip{
    width: 70px;
    height: 18px;
    font-size: 12px;
    position: absolute;
    top: 30px;
    right: 0;
    background-color: #505050;
    border-radius: 4px;
    padding: 2px 4px;
    z-index: 3;
    text-align: center;
    color: #fff;
    visibility: hidden;
}.product-input1-checkbox-icon.ask-icon:hover .product-tooltip{
  visibility: visible;
  opacity: 1;
  right: 20px;
  width: 60px;
}.product-input1-checkbox-icon.question-icon:hover .product-tooltip{
  visibility: visible;
  opacity: 1;

}
</style>

