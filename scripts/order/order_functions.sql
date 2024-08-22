-- Get all orders --

create or replace procedure get_all_orders(u_id uuid)
language plpgsql as 
$$
begin
	-- if user_id exist?	
	if not exists (select * from orders where user_id =u_id) then raise exception 'User does not exist';
	end if;
end;
$$;