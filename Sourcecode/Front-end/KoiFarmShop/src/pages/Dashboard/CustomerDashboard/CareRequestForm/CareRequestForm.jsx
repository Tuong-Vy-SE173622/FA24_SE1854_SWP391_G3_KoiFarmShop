// // CareRequestForm.js
// import React from "react";
// import { Form, Input, Button, message } from "antd";
// import { createCareRequest } from "../../../../services/CareRequestService";
// import { useNavigate } from "react-router-dom";

// const CareRequestForm = () => {
//   const [form] = Form.useForm();
//   const navigate = useNavigate();

//   const onFinish = async (values) => {
//     try {
//       const response = await createCareRequest({
//         customerId: values.customerId,
//         koiId: values.koiId,
//         status: values.status,
//         note: values.note,
//         createdBy: values.createdBy,
//       });
//       console.log(response);

//       alert("Gửi yêu cầu chăm sóc thành công!");
//       console.log("Navigating to requestId:", response.requestId);
//       navigate(`/dashboard/care-request-detail/${response.requestId}`);
//       form.resetFields();
//     } catch (error) {
//       message.error("Có lỗi xảy ra, vui lòng thử lại!");
//       console.error(error);
//     }
//   };

//   return (
//     <div
//       style={{
//         display: "flex",
//         justifyContent: "center",
//         // alignItems: "center",
//         // minHeight: "100vh",
//         padding: "24px",
//         backgroundColor: "#f0f2f5",
//       }}
//     >
//       <Form
//         form={form}
//         layout="horizontal"
//         labelCol={{ span: 6 }}
//         wrapperCol={{ span: 18 }}
//         onFinish={onFinish}
//         initialValues={{
//           customerId: 1,
//           koiId: 0,
//           status: "",
//           note: "",
//           createdBy: "",
//         }}
//         style={{
//           padding: "24px",
//           background: "white",
//           borderRadius: "8px",
//           boxShadow: "0 4px 12px rgba(0, 0, 0, 0.1)",
//           maxWidth: "500px",
//           width: "100%",
//         }}
//       >
//         <h1
//           style={{
//             fontSize: 18,
//             fontWeight: 600,
//             textAlign: "center",
//             marginBottom: 20,
//           }}
//         >
//           Form yêu cầu chăm sóc
//         </h1>
//         {/* <Form.Item
//           name="customerId"
//           label="Customer ID"
//           rules={[{ required: true, message: "Vui lòng nhập Customer ID!" }]}
//           style={{ marginBottom: 0 }} // Tắt margin bottom
//         >
//           <Input type="number" />
//         </Form.Item>

//         <Form.Item
//           name="koiId"
//           label="Koi ID"
//           rules={[{ required: true, message: "Vui lòng nhập Koi ID!" }]}
//           style={{ marginBottom: 0 }}
//         >
//           <Input type="number" />
//         </Form.Item> */}

//         <Form.Item
//           name="customerId"
//           label="Customer ID"
//           rules={[
//             { required: true, message: "Vui lòng nhập Customer ID!" },
//             {
//               validator: (_, value) =>
//                 value > 0
//                   ? Promise.resolve()
//                   : Promise.reject("Customer ID phải lớn hơn 0!"),
//             },
//           ]}
//           style={{ marginBottom: 0 }}
//         >
//           <Input type="number" />
//         </Form.Item>

//         <Form.Item
//           name="koiId"
//           label="Koi ID"
//           rules={[
//             { required: true, message: "Vui lòng nhập Koi ID!" },
//             {
//               validator: (_, value) =>
//                 value > 0
//                   ? Promise.resolve()
//                   : Promise.reject("Koi ID phải lớn hơn 0!"),
//             },
//           ]}
//           style={{ marginBottom: 0 }}
//         >
//           <Input type="number" />
//         </Form.Item>
//         <Form.Item
//           name="status"
//           label="Status"
//           rules={[{ required: true, message: "Vui lòng nhập Status!" }]}
//           style={{ marginBottom: 0 }}
//         >
//           <Input />
//         </Form.Item>

//         <Form.Item
//           name="note"
//           label="Note"
//           rules={[{ required: true, message: "Vui lòng nhập Note!" }]}
//           style={{ marginBottom: 0 }}
//         >
//           <Input />
//         </Form.Item>

//         <Form.Item
//           name="createdBy"
//           label="Created By"
//           rules={[{ required: true, message: "Vui lòng nhập Created By!" }]}
//           style={{ marginBottom: 0 }}
//         >
//           <Input />
//         </Form.Item>

//         <Form.Item
//           wrapperCol={{ offset: 6, span: 18 }}
//           style={{ marginBottom: 0 }}
//         >
//           <Button type="primary" htmlType="submit">
//             Gửi Yêu Cầu
//           </Button>
//         </Form.Item>
//       </Form>
//     </div>
//   );
// };

// export default CareRequestForm;

import { Form } from "antd";
import React from "react";

function CareRequestForm() {
  const [form] = Form.useForm();
  return <div>CareRequestForm</div>;
}

export default CareRequestForm;
