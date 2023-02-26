import {t} from "i18next";
import {Button, Form, Icon} from "semantic-ui-react";
import React, {useRef} from "react";
import {ImageFile} from "../../../models/common/ImageFile";

export interface ImagesFormProps {
    passedImages: ImageFile[],
    setPassedImages:  React.Dispatch<React.SetStateAction<ImageFile[]>>
}

export const ImagesForm = (props: ImagesFormProps) => {
    const hiddenUploadInput = useRef<any>(null)

    const uploadImages = (e: any) => {
        const files = Array.from(e.target.files);
        const newImages = files.map((file: any) => {
            return {
                path: URL.createObjectURL(file),
                formFile: file
            } as ImageFile
        })
        props.setPassedImages(oldImages => [...oldImages,...newImages] );
    }

    const changeImageOrder = (currentIndex: number, direction: number) => {
        const currentImage = props.passedImages[currentIndex];
        const directionNextImage =  props.passedImages[currentIndex + direction];

        props.setPassedImages(props.passedImages.map((item,i)=> {
            if(currentIndex === i){
                return directionNextImage;
            }
            if (currentIndex + direction === i) {
                return currentImage;
            }
            return item;
        }));
    }

    const toggleImageDeleted = (index: number) => {
        props.setPassedImages(props.passedImages.map((item,i)=> {
            if(index === i){
                item.isDeleted = !item.isDeleted;
            }
            return item;
        }));
    }
    
    
    return <Form.Field>
        <label>{t("offers:images:title")}</label>
        <Button onClick={() => hiddenUploadInput?.current?.click()}>{t('offers:images:upload_images')}</Button>
        <input ref={hiddenUploadInput} style={{display: 'none'}} type="file" multiple onChange={uploadImages} />
        <div className="offer-images">
            {props.passedImages.map((img, index) => {
                const src = img.formFile !== undefined ? img.path : "/legal-images/" + img.path;

                return <div key={index} className="offer-image">
                    <div className="tools">
                        <div className="change-order-buttons" title={t("offers:images:change_images_order")||''}>
                            {index !== 0 ? <Icon name='angle left' onClick={() => changeImageOrder(index, -1)}/> :
                                <Icon name='angle left' style={{ opacity: 0.4, pointerEvents: 'none'}}/>
                            }
                            {index !== props.passedImages.length - 1 ? <Icon name='angle right' onClick={() => changeImageOrder(index, 1)}/> :
                                <Icon name='angle right' style={{ opacity: 0.4, pointerEvents: 'none'}}/>
                            }
                        </div>
                        <div className="remove-buttons">
                            {!img.isDeleted ? <Icon title={t("offers:images:delete_image")||''} onClick={() => toggleImageDeleted(index)} name='trash alternate' />
                                : <Icon title={t("offers:images:restore_image")||''} onClick={() => toggleImageDeleted(index)} name='redo' />}

                        </div>
                    </div>
                    <img style={ img.isDeleted ? { opacity: 0.5} : {}} alt="" src={src}/>
                </div>
            })}
        </div>
    </Form.Field>
}