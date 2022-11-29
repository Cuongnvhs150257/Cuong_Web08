<template>
        <input :type="typeInput" :style="style" class="item-input" :class="{ 'item-input-red': !inValue, 'item-input-green': !inValueChange }" :placeholder="placeholder"  :value="modelValue" @input="handleInput" :tabindex="tab" :maxlength="maxlength">
</template>
<style>

    .item-input{
        height: 28px;
        width: 100%;
        border: 1px solid black;
        margin-top: 5px;
        outline: none;
        padding-left: 10px; 
        border-radius: 3px;
    }
    .item-input:focus{
        border: 1px solid #50B83C;
        outline: none;
    }

    
</style>

<script>

export default {
    
    props:{
        modelValue: String,
        toolTip: String,
        inValue: {
            type: Boolean,
            default: true,
        },
        tab: Number,
        refs: String,
        maxlength: Number,
        typeInput: String,
        NumberDecimal: Boolean,
        style: String,
    },
    methods:{
         /**
         * hàm lưu giá trị nhập
         * Nguyễn Văn Cương 26/09/2022
         */
        handleInput(event){
            console.log(1);
            this.inValueChange = this.inValue;
            this.ValueInput = event.target.value;
            if(this.NumberDecimal == true){
                this.ValueInput = this.SumFormat(this.ValueInput);
                this.$emit("update:modelValue",this.ValueInput);
            }else{
                this.$emit("update:modelValue",this.ValueInput);
            }
            this.$emit("updateAlert");
        },
        /**
        * Hàm format tổng
        * Nguyễn Văn Cương 21/11/2022
        */
        SumFormat(price) {
            if(price){
                const pieces = parseFloat(price).toFixed(2).split('')
                let ii = pieces.length - 3
                while ((ii-=3) > 0) {
                pieces.splice(ii, 0, ',')
            }
            return pieces.join('');
        }else{
            return "";
        }
    },
    },
    data(){
        return{
            //trạng thái đỏ input
            inValueChange: {
                type: Boolean,
                default: true,
            },
            //lưu giá trị input
            ValueInput: null,
            //lưu thời gian delay khi nhập
            timeout: null,

        }
    }

    
}
</script>