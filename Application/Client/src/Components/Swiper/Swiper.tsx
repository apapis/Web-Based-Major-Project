import { useEffect, useRef, forwardRef } from "react";
import { register } from "swiper/element/bundle";
import {
  NavigationButton,
  StyledSwiperContainer,
  SwiperWrapper,
} from "./Swiper.style";

type SwiperProps = React.PropsWithChildren<{
  breakpoints: any;
}>;

const SwiperContainer = forwardRef<
  HTMLDivElement,
  React.HTMLProps<HTMLDivElement>
>((props, ref) => <StyledSwiperContainer {...props} ref={ref} />);

export function Swiper(props: SwiperProps) {
  const swiperRef = useRef<HTMLDivElement>(null);
  const { children, breakpoints } = props;

  useEffect(() => {
    register();

    const params = {
      breakpoints,
      navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
      },
    };

    if (swiperRef.current) {
      Object.assign(swiperRef.current, params);
      swiperRef.current.initialize();
    }
  }, [breakpoints]);

  return (
    <SwiperWrapper>
      <SwiperContainer init="false" ref={swiperRef}>
        {children}
      </SwiperContainer>
      <NavigationButton className="swiper-button-prev"></NavigationButton>
      <NavigationButton className="swiper-button-next"></NavigationButton>
    </SwiperWrapper>
  );
}

export function SwiperSlide(props: React.HTMLAttributes<HTMLDivElement>) {
  const { children, ...rest } = props;
  return <swiper-slide {...rest}>{children}</swiper-slide>;
}
