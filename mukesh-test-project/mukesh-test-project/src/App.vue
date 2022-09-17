<template>
    <b-container class="bv-example-row" fluid>
        <br />
        <br />
        <b-row>
            <b-col>
                <b-list-group>
                    <b-list-group-item>
                        <b-form-select v-model="customersOptionSelected" :options="customersOptions" @change="loadCustomerData(customersOptionSelected)"></b-form-select>
                    </b-list-group-item>
                    <b-list-group-item href="#" :class="{active: isCustomerInfoClicked}" @click="customerInfoClicked()">Customer Info</b-list-group-item>
                    <b-list-group-item href="#" :class="{active: !isCustomerInfoClicked}" @click="productDetailsClicked()">Product Details</b-list-group-item>
                </b-list-group>
            </b-col>
            <b-col cols="8">
                <div v-show="!isCustomerInfoClicked">
                    <div style="margin-top:100px;">
                        <b-container fluid>
                            <b-row>
                                <b-col>
                                    <b-button style="margin-left:-15px;" variant="success" @click="handleAdd()">Add Product</b-button>
                                </b-col>
                                <b-col><b>Products (10)</b></b-col>
                            </b-row>
                        </b-container>
                    </div>
                    <b-editable-table disableDefaultEdit
                                      :rowUpdate="rowUpdate"
                                      :editMode="'row'"
                                      bordered
                                      class="editable-table"
                                      v-model="items"
                                      :fields="fields"
                                      @input-change="handleInput"
                                      striped>
                        <template #cell(edit)="data">
                            <div v-if="data.isEdit">
                                <BIconX class="edit-icon" @click="handleSubmit(data, false)"></BIconX>
                                <BIconCheck class="edit-icon"
                                            @click="handleSubmit(data, true)"></BIconCheck>
                            </div>
                            <BIconPencil v-else
                                         class="edit-icon"
                                         @click="handleEdit(data, true)"></BIconPencil>
                        </template>
                        <template #cell(delete)="data">
                            <BIconTrash class="remove-icon"
                                        @click="handleDelete(data)"></BIconTrash>
                        </template>
                    </b-editable-table>
                </div>
                <div v-show="isCustomerInfoClicked">
                    <div style="margin-top:60px;">
                        <b-container fluid>
                            <div v-if="customerData">
                                <b-row>
                                    <b-col>CustomerID:</b-col><b-col>{{customerData.customerId}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>CustomerName:</b-col><b-col>{{customerData.customerName}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>Address1:</b-col><b-col>{{customerData.address1}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>Address2:</b-col><b-col>{{customerData.address2}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>Industry:</b-col><b-col>{{customerData.industry}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>City:</b-col><b-col>{{customerData.city}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>State:</b-col><b-col>{{customerData.state}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>ZipCode:</b-col><b-col>{{customerData.zipCode}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>Country:</b-col><b-col>{{customerData.country}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>FirstName:</b-col><b-col>{{customerData.firstName}}	</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>LastName:</b-col><b-col>{{customerData.lastName}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>Title:</b-col><b-col>{{customerData.title}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>PhoneNumber:</b-col><b-col>{{customerData.phoneNumber}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>Deleted:</b-col><b-col>{{customerData.deleted}}</b-col>
                                </b-row>
                                <b-row>
                                    <b-col>AddDateTime:</b-col><b-col>{{customerData.addDateTime}}</b-col>
                                </b-row>
                            </div>
                        </b-container>
                    </div>
                </div>
            </b-col>
        </b-row>
    </b-container>
</template>

<script>
    import BEditableTable from "bootstrap-vue-editable-table";

    import {
        BIconTrash,
        BIconPencil,
        BIconX,
        BIconCheck,
        BButton,
    } from "bootstrap-vue";

    export default {
        components: {
            BEditableTable,
            BIconX,
            BIconTrash,
            BIconPencil,
            BIconCheck,
            BButton,
        },

        data() {
            return {
                fields: [
                    { key: "delete", label: "" },
                    {
                        key: "productId",
                        label: "Product ID",
                        type: "text",
                        editable: false,
                        class: "name-col",
                        sortable: true
                    },
                    {
                        key: "productName",
                        label: "Product Name",
                        type: "text",
                        editable: true,
                        placeholder: "Enter the name of the product",
                        class: "department-col",
                    },
                    {
                        key: "productPrice",
                        label: "Product Price",
                        type: "number",
                        editable: true,
                        placeholder: "Enter the price of the product",
                        class: "age-col",
                    },
                    { key: "edit", label: "" },
                ],
                items: [],
                rowUpdate: {},
                customersOptions: null,
                customerData: null,
                customersOptionSelected: null,
                isCustomerInfoClicked: true,
                updatedRow: {},
            };
        },

        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();

        },

        methods: {
            customerInfoClicked() {
                this.isCustomerInfoClicked = true;

            },

            productDetailsClicked() {
                this.isCustomerInfoClicked = false;
            },

            loadCustomerData(id) {
                fetch('/api/CustomerInfo/' + id)
                    .then(r => r.json())
                    .then(json => {
                        this.items = json.products;
                        this.customerData = json.customer
                        return;
                    });
            },

            fetchData() {
                fetch('/api/CustomerInfo')
                    .then(r => r.json())
                    .then(json => {
                        this.customersOptions = json;
                        return;
                    });

            },

            handleAdd() {
                if (this.rowUpdate.action != "add") {
                    const newId = Date.now();
                    this.rowUpdate = {
                        edit: true,
                        id: newId,
                        action: "add",
                        data: {
                            id: newId,
                            productId: null,
                            productName: "",
                            productPrice: ""
                        },
                    };
                }
            },

            handleInput(data) {
                if (Object.keys(this.updatedRow).length === 0) {
                    this.updatedRow = {
                        ...this.items[data.index],
                        [data.field.key]: data.value,
                    };
                } else {
                    this.updatedRow = { ...this.updatedRow, [data.field.key]: data.value };
                }
            },

            handleSubmit(data, update) {
                if (update) {

                    var productId = 0;
                    if (data.item.productId && data.item.productId > 0) {
                        productId = data.item.productId;
                    }

                    // Simple POST request with a JSON body using fetch
                    const requestOptions = {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify({
                            "ProductId": productId,
                            "CustomerId": this.customersOptionSelected,
                            "ProductName": this.updatedRow.productName,
                            "ProductPrice": this.updatedRow.productPrice
                        })
                    };
                    fetch("/api/ProductDetails", requestOptions)
                        .then(response => response.json())
                        .then(newProductId => {
                            this.rowUpdate = {
                                id: data.id,
                                edit: false,
                                action: "update",
                                data: {
                                    productId: newProductId
                                }
                            };
                            data.item.productId = newProductId;
                            this.$bvToast.toast('Saved Successfully', {
                                title: "Success",
                                solid: true,
                                variant: 'info'
                            });
                        });
                }
                else {
                    if (data.item.productId && data.item.productId > 0) {
                        this.rowUpdate = {
                            edit: false,
                            id: data.id,
                            action: "cancel",
                        };
                    }
                    else {
                        this.rowUpdate = { id: data.id, action: "delete" };
                    }
                }
                this.updatedRow = {}
            },

            handleEdit(data) {
                this.rowUpdate = { edit: true, id: data.id };
            },

            handleDelete(data) {
                if (data.item.productId > 0) {
                    fetch('/api/ProductDetails/' + data.id, { method: 'DELETE' });
                }
                this.rowUpdate = { id: data.id, action: "delete" };
            },

            validateName(value) {
                if (value === '') {
                    return {
                        valid: false,
                        errorMessage: 'Please enter name'
                    }
                }
                return { valid: true };
            },
        },
    };
</script>

<style>
    .table-container {
        margin: 10px;
    }

    table.editable-table {
        margin-top: 10px;
    }

        table.editable-table td {
            vertical-align: middle;
        }

    .editable-table .data-cell {
        padding: 8px;
        vertical-align: middle;
    }

    .editable-table .custom-checkbox {
        width: 50px;
    }

    .remove-icon {
        color: red;
        cursor: pointer;
        font-size: 20px;
    }

    .edit-icon {
        color: rgb(4, 83, 158);
        cursor: pointer;
        font-size: 20px;
    }

    .name-col {
        width: 250px;
    }

    .department-col {
        width: 250px;
    }

    .age-col {
        width: 250px;
    }

    .date-col {
        width: 200px;
    }

    .is-active-col {
        width: 100px;
    }
</style>